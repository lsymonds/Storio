namespace Baseline.Filesystem
{
    /// <summary>
    /// Base class containing all properties for basic file requests that only operate against one file.
    /// </summary>
    public abstract class BaseSingleFileRequest<T> : BaseRequest<T>
        where T : BaseSingleFileRequest<T>, new()
    {
        /// <summary>
        /// Gets or sets the file path to use to perform the action against.
        /// </summary>
        public PathRepresentation FilePath { get; set; }
        
        /// <summary>
        /// Combines the paths belonging to this request with a root path, if the root path is not null.
        /// </summary>
        /// <param name="rootPath">The root path to combine the current paths with.</param>
        /// <returns>
        /// A cloned version of the current class, with the paths combined with the root path if applicable.
        /// </returns>
        internal override T CombinePathsWithRootPath(PathRepresentation rootPath)
        {
            if (rootPath == null)
            {
                return (T)this;
            }

            var cloned = CloneForPathUpdates();
            cloned.FilePath = cloned.FilePath.CombineWithBase(rootPath);
            return cloned;
        }
        
        /// <summary>
        /// Clones the current instance for path updates. Some properties do not need to be cloned (for example path
        /// representations) as they're never modified.
        /// </summary>
        /// <returns>A clone of the current instance.</returns>
        internal override T CloneForPathUpdates()
        {
            return new T
            {
                FilePath = FilePath
            };
        }
    }
}
